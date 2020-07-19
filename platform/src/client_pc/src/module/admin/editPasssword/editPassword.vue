<template>
  <el-dialog title="修改密码" :visible.sync="editVisible" width="40%">
    <el-form ref="form" :model="data" label-width="100px" :rules="rules">
      <el-row>
        <el-col>
          <el-form-item label="密码" prop="password">
            <el-input v-model="data.password" placeholder="请输入新密码" show-password></el-input>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col>
          <el-form-item label="确认密码" prop="password2">
            <el-input v-model="data.password2" placeholder="请再次输入密码" show-password></el-input>
          </el-form-item>
        </el-col>
      </el-row>
    </el-form>
    <span slot="footer" class="dialog-footer">
      <el-button @click="editVisible = false">取 消</el-button>
      <el-button type="primary" @click="savePassword">确 定</el-button>
    </span>
  </el-dialog>
</template>

<script>
export default {
  name: 'edit-password',
  data() {
    return {
      editVisible: false
    };
  },
  methods: {
    savePassword() {
      this.$refs.form.validate(resp => {
        if (resp) {
          if (this.data.password !== this.data.password2) {
            this.$message.error('两次密码不一致');
          } else {
            sp.post('api/AuthUser/EditPassword', `=${sp.encryptPwd(this.data.password)}`).then(() => {
              this.$message.success('修改密码成功');
              this.editVisible = false;
            });
          }
        } else {
          this.$message.error('请检查表单必填项');
        }
      });
    }
  }
};
</script>

<style></style>
