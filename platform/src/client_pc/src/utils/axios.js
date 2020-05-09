import axios from 'axios';
import * as sp from './common';

axios.defaults.timeout = 10000;
axios.interceptors.request.use(config => {
  config.headers.Authorization = `BasicAuth ${localStorage.getItem('Token')}`;
  return config;
});

axios.interceptors.response.use(response => Promise.resolve(response), error => {
  if (error && error.response && error.response.status) {
    switch (error.response.status) {
      case 401:
        location.href = '/#/login';
        break;
      default:
        break;
    }
  }
})

const baseUrl = localStorage.getItem('baseUrl');
if (sp.isNullOrEmpty(baseUrl)) {
  localStorage.setItem('baseUrl', window.location.host);
} else {
  axios.defaults.baseURL = baseUrl;
}

export default axios;
