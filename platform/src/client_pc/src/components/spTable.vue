<template>
  <el-table ref="table" :data="tableData" style="width: 100%" row-key="Id">
    <el-table-column
      v-for="(column, index) in columns"
      :key="index"
      :type="column.type"
      :width="column.width || 200"
      :prop="column.prop"
      :lable="column.label"
    ></el-table-column>
    <el-table-column
      v-for="(column, index) in columns"
      :key="index"
      :label="column.label"
      :prop="column.prop"
      :width="column.width"
      :sortable="column.sortable ? 'custom' : false"
    >
      <template slot-scope="scope">
        <!-- 日期 -->
        <span v-if="column.type == 'date'">{{ scope.row[column.prop] | moment('YYYY-MM-DD') }}</span>
        <span v-if="column.type == 'datetime'">{{ scope.row[column.prop] | moment('YYYY-MM-DD HH:MM') }}</span>
        <span v-else>{{ scope.row[column.prop] }}</span>
      </template>
    </el-table-column>
  </el-table>
</template>

<script>
export default {
  name: 'sp-table',
  props: {
    columns: {
      type: Array,
      default: () => []
    },
    fetchData: {
      type: Function,
      required: true
    }
  },
  data() {
    return {
      tableData: []
    };
  },
  created() {
    this.loadData();
  },
  methods: {
    loadData() {
      this.fetch().then(resp => {
        this.tableData = resp;
      });
    }
  }
};
</script>

<style></style>
