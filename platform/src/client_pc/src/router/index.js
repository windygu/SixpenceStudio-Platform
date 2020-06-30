import login from '../module/login'
import admin from '../module/admin';
import notFound from '../module/404';
import store from '../store/store';

const Vue = require('vue');
const VueRouter = require('vue-router');

Vue.use(VueRouter);

const router = new VueRouter({
  routes: [].concat(login, admin, notFound)
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
