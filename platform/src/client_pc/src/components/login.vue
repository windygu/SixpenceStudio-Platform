<template>
  <el-form ref="form" :model="data" :rules="rules" label-width="100px" class="login">
    <el-form-item class="header">
      <span>{{header}}</span>
    </el-form-item>
    <el-form-item prop="account">
      <el-input v-model="data.account" placeholder="邮箱或者手机号"></el-input>
    </el-form-item>
    <el-form-item prop="password">
      <el-input v-model="data.password" placeholder="密码" show-password></el-input>
    </el-form-item>
    <el-form-item>
      <el-button style="width:100%" type="primary" @click="signIn" :loading="loading">登录</el-button>
    </el-form-item>
  </el-form>
</template>

<script>
export default {
  name: 'login',
  props: {
    header: {
      type: String,
      default: 'Sign In'
    }
  },
  data() {
    return {
      data: {
        account: '',
        password: ''
      },
      loading: false,
      rules: {
        account: [{ required: true, message: '请输入账号', trigger: 'blur' }],
        password: [{ required: true, message: '请输入密码', trigger: 'blur' }]
      }
    }
  },
  methods: {
    signIn() {
      this.loading = true
      try {
        this.$refs.form.validate(valid => {
          if (valid) {
            setTimeout(() => {
              this.$message.success(
                `账号：${this.data.account}, 密码：${this.data.password}`
              )
              this.loading = false
            }, 300)
          } else {
            this.loading = false
          }
        })
      } catch (error) {
        this.$message.error(error)
        this.loading = false
      }
    }
  }
}
</script>

<style lang="less" scoped>
.header {
  line-height: 60px;
  span {
    width: 100%;
    font-size: 40px;
  }
}

.login {
  width: 400px;
  height: 300px;
  text-align: center;
}
</style>
