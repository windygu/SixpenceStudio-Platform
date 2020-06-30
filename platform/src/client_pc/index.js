import './src/libs';
import components from './src/components';
import router from './src/router';
import store from './src/store/store';

const Vue = require('vue');
const moment = require('moment');
const elementUI = require('element-ui');

Vue.use(elementUI);
Vue.use(moment);

Vue.prototype.$message = elementUI.Message;

const install = _Vue => {
  components.forEach(item => Vue.component(item.name, item.component));
};

const API = {
  version: process.env.VERSION,
  router,
  store,
  install
};

import edit from './src/mixins/edit';
import pagination from './src/mixins/pagination';

export { edit, pagination };

export default API;
