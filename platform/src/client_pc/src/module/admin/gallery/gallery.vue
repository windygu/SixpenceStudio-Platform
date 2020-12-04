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
