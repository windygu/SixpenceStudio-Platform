<template>
  <div class="background" :style="{ 'background-image': `url(${imageUrl})` }">
    <div class="login">
      <a-form-model ref="form" :model="data" :rules="rules" class="login__wrapper">
        <a-form-model-item>
          <span class="header">{{ header }}</span>
        </a-form-model-item>
        <a-form-model-item prop="code">
          <a-input v-model="data.code" placeholder="邮箱或者手机号">
            <a-icon slot="prefix" type="user" style="color: rgba(0,0,0,.25)" />
          </a-input>
        </a-form-model-item>
        <a-form-model-item prop="password">
          <a-input type="password" v-model="data.password" placeholder="密码" @keyup.enter.native="signIn">
            <a-icon slot="prefix" type="lock" style="color: rgba(0,0,0,.25)" />
          </a-input>
        </a-form-model-item>
        <a-form-model-item>
          <a-button style="width: 100%;" type="primary" @click="signIn" :loading="loading">登录</a-button>
        </a-form-model-item>
      </a-form-model>
    </div>
  </div>
</template>

<script>
export default {
  name: 'login',
  data() {
    return {
      header: 'Sign In',
      data: {
        code: '',
        password: ''
      },
      loading: false,
      rules: {
        code: [{ required: true, message: '请输入账号', trigger: 'blur' }],
        password: [{ required: true, message: '请输入密码', trigger: 'blur' }]
      },
      imageUrl: ''
    };
  },
  created() {
    this.test();
    this.loadBackground();
  },
  methods: {
    test() {
      sp.get('api/DataService/test').then(resp => {
        if (resp) {
          this.$router.push('admin');
        }
      });
    },
    loadBackground() {
      sp.get('api/DataService/GetRandomImage')
        .then(image => {
          this.imageUrl = JSON.parse(image).imgurl;
        })
        .catch(() => this.$message.error('背景加载失败'));
    },
    signIn() {
      this.loading = true;
      try {
        this.$refs.form.validate(async valid => {
          if (valid) {
            const key = await sp.get('api/DataService/GetPublicKey');
            const url = 'api/AuthUser/login';
            const data = {
              code: this.data.code,
              password: sp.encrypt(this.data.password, key),
              publicKey: key
            };
            var that = this;
            sp.post(url, data).then(resp => {
              if (resp.result) {
                localStorage.setItem('Token', resp.Ticket);
                localStorage.setItem('UserId', resp.UserId);
                that.$store.commit('changeLogin', true);
                that.$router.push('admin');
                that.$message.success('登录成功');
              } else {
                that.$message.error('账号密码错误');
              }
            });
          }
        });
      } catch (error) {
        this.$message.error(error);
      } finally {
        this.loading = false;
      }
    }
  }
};
</script>

<style lang="less" scoped>
.background {
  width: 100%;
  height: 100%;
  background-size: 100% 100%;
}
.login {
  width: 400px;
  position: absolute;
  left: calc(~'50%' - 200px);
  top: 200px;
  border: 1px solid #d1d1d1;
  border-radius: 5%;
  background: #f7f1f5;
  filter: alpha(Opacity=60);
  -moz-opacity: 0.6;
  opacity: 0.6;
  .login__wrapper {
    width: 100%;
    height: 100%;
    text-align: center;
    padding: 0 20px;
    .header {
      line-height: 60px;
      font-size: 40px;
    }
  }
}
.login:hover {
  opacity: 1;
  transition: all 800ms;
}
/deep/.el-form-item__content {
  margin: 5px 10px;
}
</style>
