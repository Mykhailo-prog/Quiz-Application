<template src="./AnswerInput.html"></template>
<script>
import { mapActions } from "vuex";
export default {
  props: {
    editAnswer: Object,
    AnswerPos: {
      type: Number,
    },
  },
  data() {
    return {
      AnswerVisible: true,
      NewAnswer: { Ans: null },
    };
  },
  methods: {
    parseAnswer(answer) {
      this.NewAnswer.Ans = answer.ans;
    },
  },
  mounted() {
    if (this.editAnswer) {
      this.parseAnswer(this.editAnswer);
    }
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
