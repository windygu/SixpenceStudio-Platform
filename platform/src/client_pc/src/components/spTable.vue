<template>
  <el-table ref="table" :data="tableData" style="width: 100%" row-key="Id">
    <el-table-column
      v-for="(column, index) in columns"
      :key="index"
      :label="column.label"
      :prop="column.prop"
      :width="column.width"
      :sortable="column.sortable ? 'custom' : false"
    >
      <template slot-scope="scope">
        <span v-if="index == 0">
          <a class="compute-span" href="javascript:;" @click.stop.prevent="handleClick(scope.row)">{{ scope.row[column.prop] }}</a>
        </span>
        <span v-else-if="column.type == 'date'">{{ scope.row[column.prop] | moment('YYYY-MM-DD') }}</span>
        <span v-else-if="column.type == 'datetime'">{{ scope.row[column.prop] | moment('YYYY-MM-DD HH:MM') }}</span>
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
      this.fetchData().then(resp => {
        this.tableData = resp;
      });
    },
    handleClick(row) {
      this.$emit('link-click', row);
    }
  }
};
</script>

<style lang="less">
.compute-span {
  color: #409eff;
  text-decoration: none;
}
</style>
