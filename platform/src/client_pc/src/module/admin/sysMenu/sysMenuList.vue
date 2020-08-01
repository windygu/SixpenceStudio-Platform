<template>
  <div>
    <sp-header slot="header">
      <sp-button-list :buttons="buttons"></sp-button-list>
    </sp-header>
    <a-table :columns="columns" :data-source="data" :loading="loading">
      <a slot="name" slot-scope="text, record" @click="handleClick(record)">{{ text }}</a>
      <span slot="createdOn" slot-scope="createdOn">{{ createdOn | moment('YYYY-MM-DD HH:mm') }}</span>
      <span slot="modifiedOn" slot-scope="modifiedOn">{{ modifiedOn | moment('YYYY-MM-DD HH:mm') }}</span>
    </a-table>
    <a-modal v-model="editVisible" title="编辑" @ok="save" width="60%" okText="确认" cancelText="取消">
      <component ref="edit" v-if="editVisible" :is="editComponent" :related-attr="relatedAttr"></component>
    </a-modal>
  </div>
</template>

<script>
const columns = [
  {
    title: '菜单名',
    dataIndex: 'name',
    scopedSlots: { customRender: 'name' }
  },
  {
    title: '路由',
    dataIndex: 'router'
  },
  {
    title: '创建人',
    dataIndex: 'createdByName'
  },
  {
    title: '创建日期',
    dataIndex: 'createdOn',
    scopedSlots: { customRender: 'createdOn' }
  },
  {
    title: '最后修改人',
    dataIndex: 'modifiedByName'
  },
  {
    title: '最后修改日期',
    dataIndex: 'modifiedOn',
    scopedSlots: { customRender: 'modifiedOn' }
  },
  {
    title: '状态',
    dataIndex: 'stateCodeName'
  }
];

export default {
  name: 'sysMenuList',
  data() {
    return {
      controllerName: 'SysMenu',
      data: [],
      buttons: [{ icon: 'plus', text: '', operate: this.createData }, { icon: 'delete', text: '', operate: this.deleteData }],
      editComponent: () => import('./sysMenuEdit'),
      editVisible: false,
      relatedAttr: null,
      columns,
      loading: false
    };
  },
  created() {
    this.loadData();
  },
  methods: {
    loadData() {
      if (this.loading) {
        return;
      }
      this.loading = true;
      sp.get('api/sysmenu/GetDataList')
        .then(resp => {
          this.data = resp;
        })
        .finally(() => {
          setTimeout(() => {
            this.loading = false;
          }, 500);
        });
    },
    save() {
      this.$refs.edit.confirm();
      this.editVisible = false;
      this.loadData();
    },
    handleClick(row) {
      this.relatedAttr = {
        id: row.Id
      };
      this.editVisible = true;
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
      this.$confirm({
        title: '提示',
        content: '此操作将永久删除该菜单, 是否继续?',
        onOk: () => {
          const ids = this.selections.map(item => {
            return item.Id;
          });
          sp.post(`api/${this.controllerName}/DeleteData`, ids).then(() => {
            this.$message({
              type: 'success',
              message: '删除成功!'
            });
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

<style>
.compute-span {
  color: #409eff;
  text-decoration: none;
}
</style>
