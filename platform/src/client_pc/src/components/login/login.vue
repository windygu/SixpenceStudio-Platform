<template>
  <el-form ref="form" :model="form" label-width="80px" style="width:400px">
    <el-form-item label="账号">
      <el-input v-model="form.account"></el-input>
    </el-form-item>
    <el-form-item label="密码">
      <el-input type="password" v-model="form.password"></el-input>
    </el-form-item>
    <el-form-item>
      <el-button style="width:100%" type="primary" @click="signIn">登录</el-button>
    </el-form-item>
  </el-form>
</template>

<script>
export default {
  name: 'login',
  data () {
    return {
      form: {
        account: '',
        password: ''
      }
    }
  },
  methods: {
    checkAccount () {
      let errorMsg = '';
      if (this.form.account === '') {
        errorMsg = '账号不能为空';
      }
      if (this.form.password === '') {
        errorMsg = '密码不能为空';
      }
      return errorMsg;
    },
    signIn () {
      alert(`账号：${this.form.account}, 密码：${this.form.password}`)
    }
  }
}
</script>

<style>
</style>
