<template>
  <div class="header">
    <slot />
  </div>
</template>

<script>
export default {
  name: 'spHeader',
  data() {
    return {};
  }
};
</script>

<style lang="less" scoped>
.header {
  display: flex;
  justify-content: space-between;
  line-height: 60px;
  height: 60px;
}
</style>
