<template>
  <div>
    <el-table :data="data">
      <el-table-column prop="title" label="标题">
      </el-table-column>
      <el-table-column prop="date" label="日期" width="140"></el-table-column>
      <el-table-column prop="name" label="作者" width="120"></el-table-column>
      <el-table-column fixed="right" label="操作" width="100">
        <template slot-scope="scope">
          <el-button @click="handleClick(scope.row)" type="text" size="small">查看</el-button>
          <el-button type="text" size="small">编辑</el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script>
export default {
  name: 'spBlogTable',
  props: {
    data: { type: Array, default: () => [] }
  },
  methods: {
    handleClick(row) {
      if (!sp.isNullOrEmpty(row.Id)) {
        this.$router.push(`/blog?id=${row.Id}`);
      } else {
        this.$message.error('查看失败');
      }
    }
  }
};
</script>

<style>
</style>
