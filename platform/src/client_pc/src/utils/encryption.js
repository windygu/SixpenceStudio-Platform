export function encryptPwd(str) {
  const JSEncrypt = require('jsencrypt');
  let jse = new JSEncrypt();
  jse.setPublicKey('C0536798-3187-47F3-BF34-95596C9338BA');
  return jse.encrypt(str);
};
