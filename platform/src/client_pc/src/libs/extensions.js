/**
 * 获取 html 文本内容
 */
String.prototype.toText = function () {
  if (this && !rt.isNilOrWhiteSpace(this)) {
    const result = this.replace(/<([^>]+)>([\d\D]*?)<\/\1>/g, '$2 ').split(/\s+/);
    result.pop();   // 去掉最后一个空格
    return result.join();
  }
  return '';
}