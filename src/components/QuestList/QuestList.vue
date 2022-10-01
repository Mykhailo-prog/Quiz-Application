<template src="./QuestList.html"></template>
<script>
import Pugination from "@/components/Pugination";
import { mapGetters, mapActions } from "vuex";
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
      usersAnswer: "",
    };
  },
  methods: {
    ...mapActions(["getUserAnswer"]),
    getAnswer(answer) {
      this.usersAnswer = answer.ans;
      this.getUserAnswer({
        answer: answer.ans,
        index: this.PuginationCounter,
        quests: this.Questions,
      });
    },
  },
  computed: {
    ...mapGetters(["PuginationCounter", "Answers", "Questions"]),
  },
  components: {
    Pugination,
  },
};
</script>
<style scoped src="./QuestList.css"></style>
