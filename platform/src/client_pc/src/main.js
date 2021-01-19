// 引入 vue 和 app
import App from './App';
import components from './components';
import myRouter from './router';
import store from './store/store';
import 'web-core';

const Vue = require('vue');
const VueRouter = require('vue-router');
const Vuex = require('vuex');
const moment = require('moment');
const antd = require('antd');

// 第三方组件需要use
const install = _Vue => {
  components.forEach(item => {
    _Vue.component(item.name, item.component);
  });
};
Vue.use(antd);
Vue.use(moment);
Vue.use(install);
Vue.use(Vuex);

Vue.filter('moment', (data, formatStr) => (sp.isNullOrEmpty(data) ? '' : moment(data).format(formatStr)));
Vue.prototype.$moment = moment;

Vue.config.productionTip = false;

const router = new VueRouter({
  routes: [
    {
      // 顶层
      path: '/',
      component: App,
      children: myRouter.options.routes
    }
  ]
});

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  components: { App },
  template: '<App/>'
});
