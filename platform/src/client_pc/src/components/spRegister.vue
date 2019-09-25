<template>
  <el-form ref="form" :model="attributes" label-width="90px" class="register">
    <span class="header">
      {{header}}
    </span>
    <div v-for="(item, index) in this.attributes" :key='index'>
      <el-form-item :label="item.name">
        <el-input v-model="item.value" :type="item.type"></el-input>
      </el-form-item>
    </div>
    <el-form-item>
      <el-button style="width=100%" type="primary" @click="check">注册</el-button>
    </el-form-item>
  </el-form>
</template>

<script>
export default {
  name: 'spRegister',
  props: {
    header: {
      type: String,
      default: 'Sign Up'
    },
    attributes: {
      type: Object,
      default () {
        return [{ name: '昵称', type: 'text', value: '' }, { name: '邮箱', type: 'email', value: '' }, { name: '密码', type: 'password', value: '' }];
      }
    }
  },
  data () {
    return {

    }
  },
  methods: {
  }
}
</script>

<style scoped>
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
}
</style>
