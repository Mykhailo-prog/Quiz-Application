<template src="./TestForm.html"> </template>
<script>
import QuestInput from "@/components/QuestInput";
import validation from "@/formValidation";
import { mapActions, mapGetters } from "vuex";

export default {
  props: {
    editTest: Object,
  },
  data() {
    return {
      QuestCounter: 1,
      NewTest: { name: "", Questions: [] },
      testValid: false,
    };
  },
  methods: {
    ...mapActions(["postNewTest", "loadTests", "updateTest"]),
    async CreateTest() {
      if (this.testValid) {
        await this.postNewTest({
          test: this.NewTest,
          user: this.CurrentUser,
        });
        await this.loadTests();
        this.$bvModal.hide("modal-scoped");
      }
    },
    async EditTest() {
      if (this.testValid) {
        await this.updateTest({
          test: this.NewTest,
          testId: this.editTest.testId,
        });
        await this.loadTests();
        this.$bvModal.hide("modal-scoped" + this.editTest.testId);
      }
    },
    parseTest(test) {
      this.QuestCounter = test.questions.length;
      this.NewTest.name = test.name;
    },
    checkQuest(quests, id, valid) {
      this.NewTest.Questions[id] = quests;
      this.testValid = valid;
    },
    AnswerValidation(valid) {
      this.testValid = valid;
    },
    delQuest() {
      this.QuestCounter--;
      this.NewTest.Questions.pop();
    },
  },
  computed: {
    ...mapGetters(["CurrentUser"]),
    testValidation() {
      return validation.testValidation(this.NewTest.name);
    },
  },
  mounted() {
    if (this.editTest) {
      this.parseTest(this.editTest);
    }
  },
  watch: {
    NewTest: {
      handler: function() {
        this.testValid = validation.testValidation(this.NewTest.name);
        //this.AnswerValidation();
      },
      deep: true,
    },
  },
  components: {
    QuestInput,
  },
};
</script>
<style scoped src="./TestForm.css"></style>
