<template>
  <sp-table :columns="columns" :fetchData="fetchData" :pagination="false">
    <el-table-column label="操作">
      <template slot-scope="scope">
        <el-button size="mini" type="success" @click="start(scope.row)">运行</el-button>
      </template>
    </el-table-column>
  </sp-table>
</template>

<script>
export default {
  name: 'job',
  data() {
    return {
      controllerName: 'job',
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'lastRunTime', label: '上次运行时间', type: 'datetime' },
        { prop: 'nextRunTime', label: '下次运行时间', type: 'datetime' },
        { prop: 'runTime', label: '执行计划' },
        { prop: 'description', label: '描述' }
      ]
    };
  },
  methods: {
    fetchData() {
      return sp.get(`api/${this.controllerName}/GetDataList`).then(resp => resp);
    },
    start(row) {
      this.$confirm({
        title: '提示',
        content: '是否确认运行该作业?',
        onOk() {
          sp.get(`api/${this.controllerName}/StartJob?name=${row.name}`)
            .then(() => {
              this.$message.success('执行成功');
            })
            .catch(error => {
              this.$message.error(error);
              return Promise.reject;
            });
        }
      });
    }
  }
};
</script>

<style></style>
