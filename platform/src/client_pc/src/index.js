// 引入 vue 和 app
import Vue from 'vue';
import App from './App';
import router from './router';
import elementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
import './libs';
import components from './components';

// 第三方组件需要use
Vue.use(elementUI)
Vue.use(components)

// 注册 message
Vue.prototype.$message = elementUI.Message;

Vue.config.productionTip = false

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
})
