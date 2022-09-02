<template src="./Tests.html"></template>
<script>
import TestList from "@/components/TestList";
import UserTests from "@/components/UserTests";
import { mapState, mapActions } from "vuex";
export default {
  name: "Tests",
  data() {
    return {
      navItem: 1,
    };
  },
  methods: {
    ...mapActions(["loadTests", "getQuestions", "delTest"]),
    getQuest() {
      this.GetQuestions;
    },
    deleteTest(id) {
      this.delTest(id);
    },
  },
  computed: {
    ...mapState({
      tests: (state) => state.tests.testList,
      userTests: (state) =>
        state.tests.testList.filter(
          (test) => test.userCreatedTest[0].userId === state.users.currUser.id
        ),
      quests: (state) => state.tests.currTest.questions,
    }),
    //Можна ініціалізувати через currTest!
    GetQuestions() {
      this.getQuestions(this.quests);
      return true;
    },
    GetTests() {
      return this.tests;
    },
  },
  mounted() {
    this.loadTests();
    this.GetTests;
  },
  components: {
    TestList,
    UserTests,
  },
};
</script>
<style scoped src="./Tests.css"></style>
