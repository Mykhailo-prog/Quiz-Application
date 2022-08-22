<template src="./QuestList.html"></template>
<script>
import questions from "@/store/questions";
import Pugination from "@/components/Pugination";
import { mapState, mapActions } from "vuex";
export default {
  props: {
    question: {
      type: Object,
      required: true,
      default: () => {},
    },
  },
  data() {
    return {
      //Answers: [],
      usersAnswer: "",
      currQuest: {},
      //currQuestId: 0,
    };
  },
  methods: {
    ...mapActions(["getUserAnswer", "check"]),

    getAnswer(answer) {
      //this.currQuestId = answer.questionId;
      this.usersAnswer = answer.ans;
      this.getUserAnswer({
        answer: this.usersAnswer,
        index: this.getAnswerPos,
      });
      this.check(this.getQuests);
      //this.Answers[parseInt(answer.questionId, 10) - 1] = answer.answer;
      //this.checkFinish();
    },
    checkFinish() {
      this.$emit("checkFin", this.Answers);
    },
  },
  computed: {
    ...mapState({
      quests: (state) => state.questions.questList,
    }),
    getAnswerPos() {
      return this.quests.indexOf(this.currQuest);
    },
    getQuests() {
      return this.quests;
    },
  },
  components: {
    Pugination,
  },
  watch: {
    currQuestId: function() {
      this.usersAnswer = "";
    },
  },
};
</script>
<style scoped src="./QuestList.css"></style>
