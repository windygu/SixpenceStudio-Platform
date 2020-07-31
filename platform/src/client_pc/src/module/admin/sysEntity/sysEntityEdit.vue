<template>
  <a-form-model ref="form" :model="data" label-width="80px" :rules="rules">
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="名称" prop="name">
          <a-input v-model="data.name"></a-input>
        </a-form-model-item>
      </a-col>
      <a-col :span="12">
        <a-form-model-item label="编码" prop="code">
          <a-input v-model="data.code"></a-input>
        </a-form-model-item>
      </a-col>
    </a-row>
    <template v-if="pageState == 'edit'">
      <a-button size="mini" type="primary" style="margin-left: 20px;" @click="editVisible = true">新增</a-button>
      <a-button size="mini" type="primary" style="margin-left: 20px;" @click="addSystemAttrs">添加系统字段</a-button>
      <el-table :data="attrs" style="width: 100%; padding: 0 20px 40px 20px;">
        <el-table-column label="名称" prop="name"> </el-table-column>
        <el-table-column label="编码" prop="code"> </el-table-column>
        <el-table-column label="类型" prop="attr_type"> </el-table-column>
        <el-table-column label="长度" prop="attr_length"> </el-table-column>
        <el-table-column label="必填" prop="isrequire">
          <template slot-scope="scope">
            <span>{{ scope.row.isrequire === 0 ? '否' : '是' }}</span>
          </template>
        </el-table-column>
        <el-table-column label="操作">
          <template slot-scope="scope">
            <el-button size="mini" type="danger" @click="handleDelete(scope.$index, scope.row)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
    </template>
    <a-modal title="编辑" v-model="editVisible" width="60%">
      <sys-attrs-edit :parent="parentObj" @close="handleClose"></sys-attrs-edit>
    </a-modal>
  </a-form-model>
</template>

<script>
import edit from '../../../mixins/edit';
import sysAttrsEdit from './sysAttrsEdit';

export default {
  name: 'sysEntityEdit',
  components: { sysAttrsEdit },
  mixins: [edit],
  data() {
    return {
      controllerName: 'SysEntity',
      attrs: [],
      editVisible: false,
      rules: {
        name: [{ required: true, message: '请输入实体名', trigger: 'blur' }],
        code: [{ required: true, message: '请输入编码', trigger: 'blur' }]
      }
    };
  },
  computed: {
    parentObj() {
      return {
        id: this.Id,
        name: this.data.name,
        entityCode: this.data.code
      };
    }
  },
  methods: {
    addSystemAttrs() {
      this.$confirm({
        title: '提示',
        content: '是否要添加系统字段?',
        ok() {
          sp.post('api/SysAttrs/AddSystemAttrs', `=${this.Id}`)
            .then(() => {
              this.loadData();
              this.$message.success('添加成功');
            })
            .catch(error => {
              this.$message.error(error);
              return Promise.reject;
            });
        },
        cancel() {
          this.$message.info('已取消');
        }
      });
    },
    handleClose() {
      this.editVisible = false;
      this.loadAttrs();
    },
    handleDelete(index, row) {
      const id = row.sys_attrsId;
      sp.post('api/SysAttrs/DeleteData', [id]).then(resp => {
        this.$message.success('删除成功');
        this.loadAttrs();
      });
    },
    loadComplete() {
      this.loadAttrs();
    },
    loadAttrs() {
      sp.get(`api/SysEntity/GetEntityAttrs?id=${this.Id}`).then(resp => {
        this.attrs = resp;
      });
    }
  }
};
</script>

<style></style>
