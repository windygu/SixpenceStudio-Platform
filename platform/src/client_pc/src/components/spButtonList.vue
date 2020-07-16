<template>
  <sp-header>
    <template v-for="(button, index) in buttons">
      <a-input-search
        v-if="button.name === 'search'"
        placeholder="请输入内容"
        style="width: 200px;"
        @click="change"
        @change="change"
        @search="onSearch"
        v-bind:key="index"
      />
      <a-button v-else :icon="button.icon" @click="handleClick(button)" v-bind:key="index" style="margin-right:10px">{{ button.text }}</a-button>
    </template>
  </sp-header>
</template>

<script>
import spHeader from './spHeader';

export default {
  name: 'spButtonList',
  props: {
    // 按钮
    buttons: {
      type: Array,
      default: () => []
    }
  },
  components: { spHeader },
  data() {
    return {
      searchValue: ''
    };
  },
  methods: {
    handleClick(button) {
      if (button.operate && typeof button.operate === 'function') {
        button.operate();
      }
    },
    change() {
      this.$emit('search-change', this.searchValue);
    }
  }
};
</script>
