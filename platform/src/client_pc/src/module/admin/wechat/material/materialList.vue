<template>
  <waterfall :line-gap="200" :watch="data">
    <!-- each component is wrapped by a waterfall slot -->
    <waterfall-slot v-for="(item, index) in data" :width="item.width" :height="item.height" :order="index" :key="item.Id">
      <img class="item" :src="item.local_url" />
    </waterfall-slot>
  </waterfall>
</template>

<script>
import Waterfall from 'vue-waterfall/lib/waterfall';
import WaterfallSlot from 'vue-waterfall/lib/waterfall-slot';

export default {
  name: 'materialList',
  components: { Waterfall, WaterfallSlot },
  data() {
    return {
      isFirstLoad: true,
      busy: false,
      editVisible: false,
      data: [],
      pageIndex: 1,
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
    window.addEventListener('scroll', () => {
      var scrollTop = document.documentElement.scrollTop || document.body.scrollTop;
      if (scrollTop + window.innerHeight >= document.body.clientHeight) {
        this.loadData();
      }
    });
  },
  beforeDestroy() {
    window.removeEventListener('scroll');
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
        return;
      }
      this.loading = true;

      if (this.pageSize * this.pageIndex >= this.total && !this.isFirstLoad) {
        return;
      }

      this.busy = true;
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
        });
    },
    async getSysParam() {
      this.materialList = await sp.get('api/SysParamGroup/GetParams?code=wechat_material_type');
    }
  }
};
</script>

<style lang="less" scoped>
.item {
  position: absolute;
  top: 5px;
  left: 5px;
  right: 5px;
  bottom: 5px;
  font-size: 1.2em;
  color: rgb(0, 158, 107);
}
.item:after {
  content: attr(index);
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  -webkit-transform: translate(-50%, -50%);
  -ms-transform: translate(-50%, -50%);
}
.wf-transition {
  transition: opacity 0.3s ease;
  -webkit-transition: opacity 0.3s ease;
}
.wf-enter {
  opacity: 0;
}
</style>
