import components from './src/components';
import router from './src/router';
import store from './src/store/store';
import 'web-core';

const Vue = require('vue');
const moment = require('moment');
const elementUI = require('element-ui');

Vue.use(elementUI);
Vue.use(moment);

const install = _Vue => {
  components.forEach(item => _Vue.component(item.name, item.component));
};

const API = {
  version: process.env.VERSION,
  router,
  store,
  install
};

import edit from './src/mixins/edit';
import select from './src/mixins/select';
import pagination from './src/mixins/pagination';
import admin from './src/module/admin/admin';

export { edit, pagination, select, admin };

export default API;
