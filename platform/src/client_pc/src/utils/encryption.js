import { JSEncrypt } from 'jsencrypt'

export function encrypt(str, publickKey) {
  const jsencrypt = new JSEncrypt();
  jsencrypt.setPublicKey(publickKey);
  return jsencrypt.encrypt(str);
};
