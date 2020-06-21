<template>
  <sp-table :columns="columns" :fetchData="fetchData" :pagination="false"></sp-table>
</template>

<script>
export default {
  name: 'job',
  data() {
    return {
      controllerName: 'job',
      columns: [{ prop: 'name', label: '名称' }]
    };
  },
  methods: {
    fetchData() {
      return sp.get(`api/${this.controllerName}/GetDataList`).then(resp => resp);
    }
  }
};
</script>

<style></style>
