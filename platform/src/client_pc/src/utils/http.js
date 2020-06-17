import axios from './axios';
import { isNull } from './common';

function _handleSuccess(res) {
  if (!res) return;

  if (!res.data) {
    return res.data;
  }

  if (res.data.ErrorCode === 0) {
    return res.data.Data || res.data;
  } else if (!isNull(res.data.ErrorCode) && !isNull(res.data.Message)) {
    return Promise.reject(new Error(res.data.Message));
  } else {
    return res.data;
  }
}

function _handleError(error) {
  let err = error.response || error;

  let errorMessage;
  if (err.status === 401) {
    errorMessage = '您没有权限访问该资源';
  } else if (err.status === 403) {
    errorMessage = '请重新登录';
  } else {
    errorMessage = err.data || err;
  }
  return errorMessage;
}

export function get(url, config) {
  return new Promise(function (resolve, reject) {
    axios
      .get(url, config)
      .then(res => {
        resolve(_handleSuccess(res));
      })
      .catch(err => {
        reject(_handleError(err));
      });
  });
}

export function post(url, data, config) {
  return new Promise(function (resolve, reject) {
    axios
      .post(url, data, config)
      .then(function (res) {
        resolve(_handleSuccess(res));
      })
      .catch(function (err) {
        reject(_handleError(err));
      });
  });
}
