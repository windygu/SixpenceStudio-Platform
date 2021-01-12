export default {
  data() {
    return {
      selectNameList: [],
      selectDataList: {}
    }
  },
  created() {
    this.loadSelectDataList();
  },
  methods: {
    loadSelectDataList() {
      sp.get(`api/SysParamGroup/GetParamsList?code=${this.selectNameList.join(',')}`).then(resp => {
        this.selectNameList.forEach((item, index) => {
          this.$set(this.selectDataList, item, resp[index]);
        });
        if (this.loadSelectDataListComplete && typeof this.loadSelectDataListComplete === 'function') {
          this.loadSelectDataListComplete();
        }
      });
    }
  }
};
