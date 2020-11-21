<template>
  <div :infinite-scroll-disabled="busy" class="content">
    <a-card hoverable class="card" v-for="item in data" :key="item.Id">
      <div v-html="handleShowImage(item.local_url)" style="width:100%;" slot="cover"></div>
    </a-card>
  </div>
</template>

<script>
import materialRead from './materialRead';
import infiniteScroll from 'vue-infinite-scroll';
import pagination from '../mixins/pagination';

export default {
  name: 'materialList',
  components: { materialRead },
  directives: { infiniteScroll },
  mixins: [pagination],
  data() {
    return {
      isFirstLoad: true,
      busy: false,
      editVisible: false,
      data: [],
      pageSize: 15,
      loading: false,
      controllerName: 'WeChatMaterial',
      operations: ['search', 'more'],
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'type', label: '类型' },
        { prop: 'createdOn', label: '创建时间', type: 'datetime' }
      ],
      materialList: [],
      searchData: {
        type: 'image'
      }
    };
  },
  created() {
    this.getSysParam();
    this.loadData();
  },
  computed: {
    customApi() {
      return `api/${this.controllerName}/GetDataList?pageIndex=$pageIndex&pagesize=$pageSize&orderBy=&searchValue=&searchList=${JSON.stringify(
        this.searchList
      )}`;
    },
    searchList() {
      return [
        {
          Name: 'type',
          Value: this.searchData.type
        }
      ];
    }
  },
  methods: {
    loadData() {
      if (this.loading) {
        this.$bus.$emit('loading-finish');
        return;
      }
      this.loading = true;

      if (sp.isNullOrEmpty(this.getDataApi)) {
        this.$bus.$emit('loading-finish');
        this.$bus.$emit('loaded-all');
        return;
      }

      if (this.pageSize * this.pageIndex >= this.total && !this.isFirstLoad) {
        this.$bus.$emit('loading-finish');
        this.$bus.$emit('loaded-all');
        return;
      }

      this.busy = true;
      this.$emit('loading');
      if (!this.isFirstLoad) {
        this.pageIndex += 1;
      }
      sp.get(this.customApi.replace('$pageSize', this.pageSize).replace('$pageIndex', this.pageIndex))
        .then(resp => {
          this.data = this.data.concat(resp.DataList);
          this.total = resp.RecordCount;
          this.isFirstLoad = false;
          this.busy = false;
        })
        .finally(() => {
          this.loading = false;
          this.$emit('loading-close');
          this.$bus.$emit('loading-finish');
        });
    },
    async getSysParam() {
      this.materialList = await sp.get('api/SysParamGroup/GetParams?code=wechat_material_type');
    }
  }
};
</script>

<style lang="less" scoped>
.content {
  display: flex;
  flex-wrap: wrap;
  .card {
    width: 100%;
    max-width: 20%;
    padding: 0 15px;
    margin: 15px 0;
    border: 0 !important;
  }
}

img {
  height: 200px;
}

.demo-infinite-container {
  border: 1px solid #e8e8e8;
  border-radius: 4px;
  overflow: auto;
  padding: 8px 24px;
  height: 300px;
}
</style>
