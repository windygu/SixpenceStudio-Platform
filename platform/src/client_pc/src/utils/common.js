/**
 * 判读传入的对象是否为 undefined 或者 null
 * @param {Object} value
 * @returns {boolean}
 */
export function isNull(value) {
  return value === undefined || value === null;
}

/**
 * 判断传入对象是否空
 */
export function isNullOrEmpty(value) {
  if (isNull(value)) {
    return true;
  }
  switch (Object.prototype.toString.call(value)) {
    case '[object String]':
      return value.trim().length === 0;
    case '[object Array]':
      return value.length === 0;
    case '[object Object]':
      return Object.keys(value).length === 0;
    default:
      throw new TypeError();
  }
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
