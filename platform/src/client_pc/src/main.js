// 引入 vue 和 app
import App from './App';
import './libs';
import components from './components';

const Vue = require('vue');

// 第三方组件需要use
Vue.use(components);

Vue.config.productionTip = false;

/* eslint-disable no-new */
new Vue({
  el: '#app',
  components: { App },
  template: '<App/>'
});
