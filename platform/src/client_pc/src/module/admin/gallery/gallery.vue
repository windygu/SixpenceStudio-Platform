<template>
  <div>
    <vue-waterfall-easy @click="showModal" :maxCols="4" style="position: absolute;width: 83%" :imgsArr="dataList" @scrollReachBottom="loadData"></vue-waterfall-easy>
    <div>
      <a-modal
        title="图片"
        :visible="visible"
        :confirm-loading="confirmLoading"
        @ok="handleOk"
        @cancel="handleCancel"
        width="70%">
        <div v-if="visible">
          <img style="width: 100%;height: 100%" :src="imgUrl"/>
        </div>
        <template slot="footer">
          <div v-if="visible">
            <a-button type="primary" @click="downloadImg">点击下载</a-button>
          </div>
        </template>
      </a-modal>
    </div>
  </div>
</template>

<script>
import vueWaterfallEasy from 'vue-waterfall-easy';

export default {
  name: 'gallery',
  components: { vueWaterfallEasy },
  data() {
    return {
      imgUrl: '',
      ModalText: '',
      visible: false,
      confirmLoading: false,
      isFirstLoad: true,
      busy: false,
      dataList: [],
      pageIndex: 1,
      pageSize: 15,
      total: 0,
      loading: false,
      controllerName: 'Gallery',
      baseUrl: sp.getBaseUrl()
    };
  },
  computed: {
    customApi() {
      return `api/${this.controllerName}/GetDataList?pageIndex=$pageIndex&pagesize=$pageSize&orderBy=&searchValue=&searchList=&viewId=`;
    }
  },
  created() {
    this.loadData();
  },
  methods: {
    showModal(event, { index, value }) {
      // 阻止a标签跳转
      event.preventDefault()
      this.imgUrl = ''
      this.imgUrl = value.infoUrl
      this.visible = true;
    },
    downloadImg () {
      window.open(this.imgUrl, '_blank')
    },
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
          this.dataList = this.dataList.concat(
            resp.DataList.map(item => {
              return {
                src: this.baseUrl + item.preview_url,
                href: 'https://www.baidu.com/',
                name: item.name,
                infoUrl: this.baseUrl + item.image_url
              };
            })
          );
          this.total = resp.RecordCount;
          this.isFirstLoad = false;
          this.busy = false;
        })
        .finally(() => {
          this.loading = false;
        });
    }
  }
};
</script>
<style>
</style>
