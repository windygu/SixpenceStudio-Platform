import { JSEncrypt } from 'jsencrypt'

/*
* 非对称加密
* @method encryptPwd
*/
export function encrypt(str, key) {
  const jsencrypt = new JSEncrypt();
  jsencrypt.setPublicKey(key);
  return jsencrypt.encrypt(str);
};

/*
* 非对称解密
* @method decrypt
*/
export function decrypt(msg, privateKey) {
  let decrypt = new JSEncrypt()
  decrypt.setPrivateKey(privateKey)
  var decryptMsg = decrypt.decrypt(msg)
  return decryptMsg
};
