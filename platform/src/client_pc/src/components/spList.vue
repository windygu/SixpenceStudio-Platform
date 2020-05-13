<template>
  <div>
    <sp-header v-if="buttons && buttons.length > 0">
      <sp-button-list :buttons="buttons"></sp-button-list>
    </sp-header>
    <sp-table
      ref="list"
      :fetchData="fetchData"
      :columns="columns"
      @link-click="handleClick"
      :allowSelect="allowSelect"
      @selection-change="handleSelectionChange"
    ></sp-table>
    <el-dialog :title="editTitle" :visible.sync="editVisible" width="60%">
      <component v-if="editVisible" :is="editComponent" @close="editVisible = false" :related-attr="relatedAttr" @load-data="loadData()"></component>
    </el-dialog>
  </div>
</template>

<script>
export default {
  name: 'sp-list',
  props: {
    // 操作按钮
    operations: {
      type: Array,
      default: () => []
    },
    // 控制器
    controllerName: {
      type: String
    },
    // 列
    columns: {
      type: Array,
      default: () => []
    },
    // 编辑页组件
    editComponent: {
      type: Object
    },
    // 编辑页标题
    editTitle: {
      type: String,
      default: '编辑'
    },
    // 是否可以选择列
    allowSelect: {
      type: Boolean,
      default: false
    },
    // 标题点击
    headerClick: {
      type: Function
    },
    // 页面类型
    type: {
      type: String,
      default: 'normal'
    }
  },
  data() {
    return {
      normalOperations: [
        { name: 'new', icon: 'el-icon-plus', operate: this.createData },
        { name: 'delete', icon: 'el-icon-delete', operate: this.deleteData }
      ],
      editVisible: false,
      relatedAttr: null,
      selections: []
    };
  },
  computed: {
    isNormal() {
      return this.type === 'normal';
    },
    buttons() {
      return this.normalOperations.filter(item => this.operations.includes(item.name));
    }
  },
  methods: {
    loadData() {
      this.$refs.list.loadData();
    },
    async fetchData() {
      return sp.get(`api/${this.controllerName}/getdatalist`);
    },
    handleClick(row) {
      if (this.isNormal) {
        this.relatedAttr = {
          id: row.Id
        };
        this.editVisible = true;
      } else {
        if (this.headerClick && typeof this.headerClick === 'function') {
          this.headerClick(row);
        }
      }
    },
    handleSelectionChange(val) {
      this.selections = val;
    },
    createData() {
      this.relatedAttr = {};
      this.editVisible = true;
    },
    deleteData() {
      if (!this.selections || this.selections.length === 0) {
        this.$message.warning('请选择一项，再进行删除');
        return;
      }
      this.$confirm('此操作将永久删除该菜单, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      })
        .then(() => {
          const ids = this.selections.map(item => {
            return item.Id;
          });
          sp.post(`api/${this.controllerName}/DeleteData`, ids).then(() => {
            this.$message.success('删除成功');
            this.loadData();
          });
        })
        .catch(() => {
          this.$message.info('已取消删除');
        });
    }
  }
};
</script>

<style></style>
