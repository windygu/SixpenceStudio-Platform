import axios from 'axios';
import * as sp from './common';

axios.defaults.timeout = 20000;
axios.defaults.withCredentials = true;
axios.interceptors.request.use(config => {
  config.headers.Authorization = `BasicAuth ${localStorage.getItem('Token') || ''}`;
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
    return Promise.reject(error);
  }
});

const baseUrl = localStorage.getItem('baseUrl');
if (sp.isNullOrEmpty(baseUrl)) {
  localStorage.setItem('baseUrl', window.location.origin);
} else {
  axios.defaults.baseURL = baseUrl;
}

export default axios;
