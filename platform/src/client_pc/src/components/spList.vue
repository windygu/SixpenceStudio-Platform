<template>
  <div>
    <sp-header v-if="buttons && buttons.length > 0">
      <sp-button-list :buttons="buttons"></sp-button-list>
    </sp-header>
    <el-table
      ref="table"
      :data="tableData"
      :style="{ 'min-height': minHeight }"
      row-key="Id"
      @selection-change="handleSelectionChange"
      v-loading="loading"
      element-loading-text="拼命加载中"
      element-loading-spinner="el-icon-loading"
    >
      <el-table-column type="selection" width="55" v-if="allowSelect"></el-table-column>
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
    <el-pagination
      background
      layout="prev, pager, next"
      @size-change="sizeChange"
      @current-change="currentPage"
      :current-page="pageIndex"
      :page-size="pageSize"
      :pager-count="pagerCount"
      :total="total"
    >
    </el-pagination>
    <el-dialog :title="editTitle" :visible.sync="editVisible" width="60%" append-to-body>
      <component v-if="editVisible" :is="editComponent" @close="editVisible = false" :related-attr="relatedAttr" @load-data="loadData()"></component>
    </el-dialog>
  </div>
</template>

<script>
import pagination from '../mixins/pagination';

export default {
  name: 'sp-list',
  mixins: [pagination],
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
    // 自定义 API
    customApi: {
      type: String,
      default: ''
    },
    minHeight: {
      type: String,
      default: '600px'
    }
  },
  created() {
    if (this.$attrs['pageSize']) {
      this.pageSize = this.$attrs['pageSize'];
    }
    this.loadData();
  },
  data() {
    return {
      tableData: [],
      normalOperations: [
        { name: 'new', icon: 'el-icon-plus', operate: this.createData },
        { name: 'delete', icon: 'el-icon-delete', operate: this.deleteData }
      ],
      editVisible: false,
      relatedAttr: null,
      selections: [],
      loading: false
    };
  },
  computed: {
    buttons() {
      return this.normalOperations.filter(item => this.operations.includes(item.name));
    }
  },
  methods: {
    currentPage(index) {
      this.pageIndex = index;
      this.loadData();
    },
    async loadData() {
      if (this.loading) {
        return;
      }
      this.loading = true;
      let url = `api/${this.controllerName}/GetDataList?searchList=&orderBy=&pageSize=${this.pageSize}&pageIndex=${this.pageIndex}`;
      if (!sp.isNullOrEmpty(this.customApi)) {
        url = this.customApi;
      }
      try {
        const resp = await sp.get(url);
        if (resp && resp.DataList) {
          this.tableData = resp.DataList;
          this.total = resp.RecordCount;
        } else {
          this.tableData = resp;
        }
      } catch (error) {
        this.$message.error(error);
      } finally {
        setTimeout(() => {
          this.loading = false;
        }, 200);
      }
    },
    handleClick(row) {
      if (this.headerClick && typeof this.headerClick === 'function') {
        this.headerClick(row);
      } else {
        this.relatedAttr = {
          id: row.Id
        };
        this.editVisible = true;
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

<style lang="less" scoped>
.compute-span {
  color: #409eff;
  text-decoration: none;
}
.el-pagination {
  text-align: center;
  padding-top: 20px;
}
</style>
