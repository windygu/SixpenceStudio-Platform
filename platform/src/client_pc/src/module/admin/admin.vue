<template>
  <a-layout class="layout-home">
    <a-layout-sider breakpoint="lg" collapsed-width="0">
      <a-menu theme="dark" mode="inline" :default-selected-keys="['1']">
        <a-sub-menu v-for="(item, index) in menus" :key="index">
          <span slot="title">
            <a-icon type="setting" /><span>{{ item.title }}</span>
          </span>
          <a-menu-item v-for="item2 in item.subMenu[0].menus" :key="`/admin/${item2.router}`" @click="handleClick">
            {{ item2.title }}
          </a-menu-item>
        </a-sub-menu>
      </a-menu>
    </a-layout-sider>
    <a-layout>
      <a-layout-header :style="{ background: '#fff', padding: '0 20px 0 0', textAlign: 'right' }">
        <a-avatar icon="user" />
      </a-layout-header>
      <a-layout-content :style="{ margin: '24px 16px 0' }">
        <div :style="{ padding: '24px', background: '#fff', minHeight: '360px' }">
          <router-view :key="$route.path"></router-view>
        </div>
      </a-layout-content>
      <a-layout-footer style="textAlign: center">
        Created by Du Miaoxin 2020
      </a-layout-footer>
    </a-layout>
  </a-layout>
</template>

<script>
export default {
  name: 'admin',
  data() {
    return {
      menus: [],
      defaultOpenedsArray: [],
      data: {},
      rules: {
        password: [{ required: true, message: '请输入密码', trigger: 'blur' }],
        password2: [{ required: true, message: '请再次输入密码', trigger: 'blur' }]
      },
      imageUrl: '' // 头像
    };
  },
  created() {
    this.getUserInfo();
    this.getMenu();
  },
  methods: {
    goHome() {
      this.$router.push({
        name: 'home'
      });
    },
    getUserInfo() {
      return sp.get(`api/UserInfo/GetData?id=${sp.getUser()}`).then(resp => {
        this.imageUrl = sp.getBaseUrl() + resp.avatarUrl;
        localStorage.setItem('Avatar', this.imageUrl);
      });
    },
    getMenu() {
      const searchList = [
        {
          Name: 'stateCode',
          Value: 1
        }
      ];
      sp.get(`api/sysmenu/getdatalist?searchList=${JSON.stringify(searchList)}`).then(resp => {
        resp.forEach(e => {
          const menu = {
            title: e.name,
            router: e.router,
            subMenu: [{ title: '', menus: [] }]
          };
          if (e.ChildMenus && e.ChildMenus.length > 0) {
            menu.subMenu[0].menus = e.ChildMenus.map(item => ({
              title: item.name,
              router: item.router
            }));
          }
          this.menus.push(menu);
        });
      });
    },
    handleClick({ item, key, keyPath }) {
      if (sp.isNullOrEmpty(keyPath[0])) {
        this.$message.error('发生错误，请检查菜单地址是否正确！');
        return;
      }
      this.$router.push({ path: keyPath[0] });
    },
    logout() {
      this.$message.success('退出成功');
      localStorage.removeItem('Token'); // 移除登录Token
      this.$store.commit('changeLogin', false); // 修改登录状态
      this.$router.replace('/login');
    }
  }
};
</script>

<style lang="less">
body,
html {
  margin: 0px;
  width: 100%;
  height: 100%;
}
</style>
<style lang="less" scoped>
.layout-home {
  height: 100%;
}
</style>
