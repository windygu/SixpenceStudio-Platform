import { JSEncrypt } from 'jsencrypt';
import md5 from 'js-md5';

export function encrypt(str, publickKey) {
  const jsencrypt = new JSEncrypt();
  jsencrypt.setPublicKey(publickKey);
  return jsencrypt.encrypt(str);
};

export function md5Encrypt(str) {
  return md5(str);
};
