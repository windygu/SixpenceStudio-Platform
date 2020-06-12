import Vue from 'vue';
import login from '../module/login'
import admin from '../module/admin';
import VueRouter from 'vue-router';
import store from '../store/store';

Vue.use(VueRouter);

const router = new VueRouter({
  routes: [].concat(login, admin)
});

router.beforeEach((to, from, next) => {
  if (to.matched.some(m => m.meta.auth)) {
    if (store.state.isLogin) {
      next();
    } else {
      next({ name: 'login' });
    }
  } else {
    next();
  }
});

// 路由配置
export default router;
