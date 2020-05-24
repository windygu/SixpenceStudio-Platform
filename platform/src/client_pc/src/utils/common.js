/**
 * 判读传入的对象是否为 undefined 或者 null
 * @param {Object} value
 * @returns {boolean}
 */
export function isNull(value) {
  return value === undefined || value === null;
}

export function isNullOrEmpty(value) {
  return isNull(value) || value.trim().length === 0;
}

export function getUser() {
  return localStorage.getItem('UserId');
}

export function getBaseUrl() {
  let url = localStorage.getItem('baseUrl');
  if (!isNullOrEmpty(url)) {
    return url.charAt(url.length - 1) === '/' ? url : url + '/';
  }
  return localStorage.getItem('baseUrl');
}
