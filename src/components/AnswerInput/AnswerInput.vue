<template src="./AnswerInput.html"></template>
<script>
import { mapActions } from "vuex";
export default {
  props: {
    AnswerPos: {
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
        this.$emit("added-ans", newVal, this.AnswerPos - 1);
      },
      immediate: true,
      deep: true,
    },
  },
};
</script>
<style scoped src="./AnswerInput.css"></style>
