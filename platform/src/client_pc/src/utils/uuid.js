import UUID from 'uuidjs';
import { isNull } from './common';

/**
 * 创建一个 Guid
 */
export function newUUID() {
  return UUID.generate();
};

/**
 * 判断 uuid 是否一致
 * @param {string} value1
 * @param {string} value2
 */
export function isSameUUID(value1, value2) {
  if (isNull(value1) || isNull(value2)) {
    return false;
  }

  return value1.replace(/[{}]/g, '').toUpperCase() === value2.replace(/[{}]/g, '').toUpperCase();
};
