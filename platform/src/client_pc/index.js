import Vue from 'vue';
import elementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
import './src/libs';
import components from './src/components';
import axios from 'axios';

// 第三方组件需要use
Vue.use(elementUI);

const install = _Vue => {
  components.forEach(item => Vue.component(item.name, item.component));
};

const API = {
  version: process.env.VERSION,
  install
};

if (!sp.isNullOrEmpty(localStorage.getItem('baseUrl'))) {
  axios.defaults.baseURL = localStorage.getItem('baseUrl');
}

export default API;
