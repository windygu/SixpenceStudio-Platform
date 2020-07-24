<template>
  <a-table :columns="columns" :data-source="data">
    <span slot="action" slot-scope="text, record">
      <a-button type="primary" @click="start(record)">运行</a-button>
    </span>
  </a-table>
</template>

<script>
const columns = [
  {
    title: '名称',
    dataIndex: 'name'
  },
  {
    title: '上次运行时间',
    dataIndex: 'lastRunTime'
  },
  {
    title: '下次运行时间',
    dataIndex: 'nextRunTime'
  },
  {
    title: '执行计划',
    dataIndex: 'runTime'
  },
  {
    title: '描述',
    dataIndex: 'description'
  },
  {
    title: 'Action',
    key: 'action',
    scopedSlots: { customRender: 'action' }
  }
];

export default {
  name: 'job',
  data() {
    return {
      columns,
      data: [],
      controllerName: 'job'
    };
  },
  created() {
    this.fetchData().then(resp => {
      this.data = resp;
    });
  },
  methods: {
    fetchData() {
      return sp.get(`api/${this.controllerName}/GetDataList`).then(resp => resp);
    },
    start(row) {
      this.$confirm({
        title: '提示',
        content: '是否确认运行该作业?',
        onOk: () => {
          sp.get(`api/${this.controllerName}/StartJob?name=${row.name}`)
            .then(() => {
              this.$message.success('执行成功');
            })
            .catch(error => {
              this.$message.error(error);
              return Promise.reject;
            });
        },
        onCancel: () => {
          this.$message.info('已取消');
        }
      });
    }
  }
};
</script>

<style></style>
