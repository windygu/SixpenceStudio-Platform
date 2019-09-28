<template>
  <el-form ref="regform" :model="regform" :rules="rules" class="register">
    <span class="header">
      {{header}}
    </span>
    <el-form-item prop="name">
      <el-input v-model="regform.name" placeholder="昵称"></el-input>
    </el-form-item>
    <el-form-item prop="email">
      <el-input v-model="regform.email" placeholder="邮箱"></el-input>
    </el-form-item>
    <el-form-item prop="password">
      <el-input v-model="regform.password" placeholder="密码" type="password"></el-input>
    </el-form-item>
    <el-form-item prop="secPassword">
      <el-input v-model="regform.secPassword" placeholder="再次输入密码" type="password"></el-input>
    </el-form-item>
    <el-form-item>
      <el-button type="primary" @click="save('regform')">注册</el-button>
      <el-button @click="reset('regform')">重置</el-button>
    </el-form-item>
  </el-form>
</template>

<script>
export default {
  name: 'spRegister',
  props: {
    // 表名
    header: {
      type: String,
      default: 'Sign Up'
    }
  },
  data () {
    let reg = /(?!^(\d+|[a-zA-Z]+|[~!@#$%^&*?]+)$)^[\w~!@#$%^&*?]{6,12}$/
    var validatePwd = (rule, value, callback) => {
      if (!reg.test(value)) {
        callback(new Error('请重新尝试“字母+数字”的6-12位组合!'));
      } else {
        callback();
      }
    }
    var validateComfirmPwd = (rule, value, callback) => {
      if (this.regform.password !== value) {
        callback(new Error('两次密码不一致'));
      } else {
        callback();
      }
    }
    return {
      regform: {
        name: '',
        email: '',
        password: '',
        secPassword: ''
      },
      rules: {
        name: [{
          required: true, message: '请输入昵称', trigger: 'blur'
        }, {
          min: 3, max: 8, message: '长度在 3 到 8 个字符', trigger: 'blur'
        }],
        email: [{
          required: true, message: '请输入邮箱', trigger: 'blur'
        }, {
          pattern: /^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,})$/, message: '请输入正确的邮箱'
        }],
        password: [{
          required: true, message: '请输入密码', trigger: ['blur', 'change']
        }, {
          trigger: 'blur', validator: validatePwd
        }],
        secPassword: [{
          required: true, message: '请再次输入密码', trigger: ['blur', 'change']
        }, {
          trigger: 'blur', validator: validateComfirmPwd
        }]
      }
    }
  },
  methods: {
    save (formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          this.$emit('reg-success', this.regform);
          this.$message({
            showClose: true,
            message: '注册成功',
            type: 'success'
          });
        } else {
          return false;
        }
      });
    },
    reset (formName) {
      this.$refs[formName].resetFields();
      this.$emit('reg-reset');
    }
  }
}
</script>

<style lang="less" scoped>
.header {
  width: 100%;
  font-size: 50px;
  bottom: 10px;
  position: relative;
}
.register {
  width: 400px;
  height: 400px;
  text-align: center;
  .el-form-item {
    margin-left: 50px;
    width: 300px;
  }
}
</style>
