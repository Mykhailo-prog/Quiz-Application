<template src="./Start.html"></template>
<script>
import QuestList from "@/components/QuestList";
import { mapState, mapActions } from "vuex";
export default {
  name: "Quiz",
  data() {
    return {};
  },
  methods: {},
  computed: {
    ...mapState({
      questions: (state) => state.questions.questList,
      count: (state) => state.questions.pugCounter,
    }),
    GetQuestion() {
      return this.questions;
    },
  },
  mounted() {},
  components: {
    QuestList,
  },
};
</script>
<style scoped src="./Start.css"></style>
