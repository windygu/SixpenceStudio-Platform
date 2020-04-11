export default {
  props: {
    relatedAttr: {
      type: Object
    }
  },
  data() {
    return {
      Id: '',
      controllerName: '',
      data: {}
    };
  },
  created() {
    if (this.relatedAttr && this.relatedAttr.id) {
      this.Id = this.relatedAttr.id;
      this.loadData();
    }
  },
  methods: {
    async loadData() {
      this.data = await sp.get(`api/${this.controllerName}/GetData?id=${this.Id}`);
      if (this.loadComplete && typeof this.loadComplete === 'function') {
        this.loadComplete();
      }
    },
    saveData() {
      const operateName = sp.isNullOrEmpty(this.Id) ? 'CreateData' : 'UpdateData';
      if (sp.isNullOrEmpty(this.Id)) {
        this.data.Id = sp.newUUID();
      }
      sp.post(`api/${this.controllerName}/${operateName}`, this.data).then(() => {
        this.$emit('close');
        this.$emit('load-data');
        this.$message.success('添加成功');
      });
    }
  }
};
