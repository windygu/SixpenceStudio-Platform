<template>
  <vue-waterfall-easy style="position: absolute" :imgsArr="dataList" @scrollReachBottom="loadData"></vue-waterfall-easy>
</template>

<script>
import vueWaterfallEasy from 'vue-waterfall-easy';

export default {
  name: 'gallery',
  components: { vueWaterfallEasy },
  data() {
    return {
      isFirstLoad: true,
      busy: false,
      dataList: [],
      pageIndex: 1,
      pageSize: 15,
      total: 0,
      loading: false,
      controllerName: 'SysFile',
      baseUrl: sp.getBaseUrl()
    };
  },
  computed: {
    customApi() {
      return `api/${this.controllerName}/GetDataList?pageIndex=$pageIndex&pagesize=$pageSize&orderBy=&searchValue=&searchList=&viewId=3BCF6C07-2B49-4D69-9EB1-A3D5B721C976`;
    }
  },
  created() {
    this.loadData();
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
          this.dataList = this.dataList.concat(
            resp.DataList.map(item => {
              return {
                src: this.baseUrl + item.DownloadUrl,
                href: 'https://www.baidu.com/',
                info: item.name
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
