<template src="./AnswerInput.html"></template>
<script>
export default {
  props: {
    AnswerCounter: {
      type: Number,
    },
  },
  data() {
    return {
      AnswerVisible: true,
    };
  },
};
</script>
<style scoped src="./AnswerInput.css"></style>
