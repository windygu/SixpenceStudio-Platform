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
      sp.get(`api/SysParamGroup/GetParamsList?code=${selectDataList.join(',')}`).then(resp => {
        selectNameList.forEach((item, index) => {
          this.selectDataList[item] = resp[index];
        });
        if (this.loadSelectDataListComplete && typeof this.loadSelectDataListComplete === 'function') {
          this.loadSelectDataListComplete();
        }
      });
    }
  }
}