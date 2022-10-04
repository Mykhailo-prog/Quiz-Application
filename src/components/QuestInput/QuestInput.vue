<template src="./QuestInput.html"></template>
<script>
import AnswerInput from "@/components/AnswerInput";
export default {
  props: {
    QuestionPos: {
      type: Number,
    },
  },
  data() {
    return {
      QuestVisible: true,
      AnswerPos: 1,
      NewQuest: {
        question: "",
        correctAnswer: "",
        testId: null,
        NewAnswers: [],
      },
      Answer: null,
    };
  },
  methods: {
    addAnswer(answer, id) {
      this.Answer = answer;
      this.NewQuest.NewAnswers[id] = answer;
      this.QuestValid();
    },
    delAnswer() {
      this.AnswerPos--;
      this.NewQuest.NewAnswers.pop();
    },
    QuestValid() {
      if (this.NewQuest.question === "" || this.NewQuest.correctAnswer === "") {
        return false;
      } else {
        return true;
      }
    },
    AnsValid() {
      if (this.NewQuest.NewAnswers.length === 0) {
        return false;
      }
      if (this.Answer.answer === null || this.Answer.answer === "") {
        return false;
      }
      let cnt = 0;
      this.NewQuest.NewAnswers.forEach((ans) => {
        if (ans.answer === this.NewQuest.correctAnswer) {
          cnt++;
        }
      });
      if (cnt === 1) {
        return true;
      } else {
        return false;
      }
    },
  },
  watch: {
    NewQuest: {
      handler: function(newVal) {
        this.$emit(
          "added-quest",
          newVal,
          this.QuestionPos - 1,
          this.AnsValid() ? this.QuestValid() : false
        );
      },
      immediate: true,
      deep: true,
    },
    Answer: {
      handler: function(newVal) {
        this.$emit("ans-valid", this.AnsValid());
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
