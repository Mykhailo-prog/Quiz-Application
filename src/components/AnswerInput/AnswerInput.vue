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
      NewAnswer: { answer: null, questionId: null },
    };
  },
  watch: {
    NewAnswer: {
      handler: function(newVal) {
        this.$emit("added-ans", newVal, this.AnswerCounter - 1);
      },
      deep: true,
    },
  },
};
</script>
<style scoped src="./AnswerInput.css"></style>
