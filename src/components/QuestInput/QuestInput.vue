<template src="./QuestInput.html"></template>
<script>
import AnswerInput from "@/components/AnswerInput";
export default {
  props: {
    counter: {
      type: Number,
    },
  },
  data() {
    return {
      QuestVisible: false,
      AnsCounter: 1,
      NewQuest: {
        question: null,
        correctAnswer: null,
        testId: null,
        NewAnswers: [],
      },
    };
  },
  methods: {
    addAns(answer, id) {
      answer.questionId = this.counter;
      this.NewQuest.NewAnswers[id] = answer;
      //this.$emit("send-ans", this.NewAnswers, this.counter - 1);
    },
    delAns() {
      this.AnsCounter--;
      this.NewAnswers.pop();
    },
  },
  watch: {
    NewQuest: {
      handler: function(newVal, oldVal) {
        this.$emit("added-quest", newVal, this.counter - 1);
      },
      deep: true,
    },
  },
  components: {
    AnswerInput,
  },
};
</script>
<style scoped src="./QuestInput.css"></style>
