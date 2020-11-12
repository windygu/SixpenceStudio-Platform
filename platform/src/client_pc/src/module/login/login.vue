<template>
  <div class="background">
    <div class="login">
      <a-form-model ref="form" :model="data" :rules="rules" class="login__wrapper">
        <a-form-model-item style="margin:0">
          <span class="header">{{ header }}</span>
        </a-form-model-item>
        <a-form-model-item style="margin-bottom: 20px;">
          <span class="desc">{{ '我想给世界留下有价值的东西' }}</span>
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
          <a-checkbox :checked="autoSignin" style="float:left">自动登录</a-checkbox>
          <a href="#/login/forget" class="forget-pwd">忘记密码</a>
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
      header: 'Sixpence Blog',
      data: {
        code: '',
        password: ''
      },
      loading: false,
      rules: {
        code: [{ required: true, message: '请输入账号', trigger: 'blur' }],
        password: [{ required: true, message: '请输入密码', trigger: 'blur' }]
      },
      autoSignin: true
    };
  },
  created() {
    this.test();
  },
  methods: {
    test() {
      sp.get('api/DataService/test').then(resp => {
        if (resp) {
          this.$router.push('admin');
        }
      });
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
              password: sp.encrypt(sp.md5Encrypt(this.data.password), key),
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
          } else {
            this.$message.error('请检查账号密码是否输入');
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
  background: #f0f2f5 url(../../../src/assets/background.svg) no-repeat 50%;
}
.login {
  width: 400px;
  position: absolute;
  left: calc(~'50%' - 200px);
  top: 200px;
  .login__wrapper {
    width: 100%;
    height: 100%;
    text-align: center;
    padding: 0 20px;
    .header {
      font-size: 40px;
      color: rgba(0, 0, 0, 0.85);
      font-family: Avenir, Helvetica Neue, Arial, Helvetica, sans-serif;
      font-weight: 600;
      line-height: 60px;
    }
    .desc {
      font-size: 14px;
      color: rgba(0, 0, 0, 0.45);
    }
    .forget-pwd {
      float: right;
      color: #52c41a;
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

/deep/ .ant-row {
  .ant-form-explain {
    text-align: left;
  }
}
</style>
