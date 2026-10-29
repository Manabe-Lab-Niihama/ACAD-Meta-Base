﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Mebiustos.MMD4MecanimFaciem {
    public class FaciemForMecanim : MonoBehaviour {

        class WeightPack {
            public int index;
            public Weight weight;
        }

        class WeightBlend {
            public float totalWeight;
            public float count;

            public float weight {
                get {
                    return totalWeight == 0 ? 0 : totalWeight / (float)count;
                }
            }
        }

        public bool UseHelper;
        public bool OverrideWeight = true;

        FaciemDatabase database;
        WeightPack[] weightPacks;

        MMD4MecanimModel mmdmodel;
        int morphDataLength;
        MMD4MecanimMorphHelper[] helpers;
        MMD4MecanimModel.Morph[] morphs;
        WeightBlend[] blends;

        void Start() {
            this.database = this.GetComponent<FaciemDatabase>();
            this.mmdmodel = this.GetComponent<MMD4MecanimModel>();
            if (this.mmdmodel.morphList == null)
                this.mmdmodel.Initialize();
            this.morphDataLength = this.mmdmodel.modelData.morphDataList.Length;

            this.weightPacks = CollectionWeightComponents();
            if (this.UseHelper) {
                this.helpers = CreateHelper();
            } else {
                this.morphs = this.mmdmodel.morphList;
            }

            this.blends = new WeightBlend[this.morphDataLength];
            for (int i = 0; i < this.blends.Length; i++) {
                this.blends[i] = new WeightBlend();
            }
        }

        void Update() {
            UpdateFace();
        }

        void UpdateFace() {
            GetWeightBlendInfos();

            if (this.UseHelper) {
                for (int i = 0; i < this.morphDataLength; i++) {
                    if (this.helpers[i] != null) {
                        this.helpers[i].morphWeight = blends[i].weight;
                        this.helpers[i].overrideWeight = this.OverrideWeight;
                    }
                }
            } else {
                var weight2 = this.OverrideWeight ? 1.0f : 0.0f;
                for (int i = 0; i < this.morphDataLength; i++) {
                    this.morphs[i].weight = blends[i].weight;
                    this.morphs[i].weight2 = weight2;
                }
            }
        }

        void GetWeightBlendInfos() {
            // initialize
            for (int i = 0; i < this.blends.Length; i++) {
                this.blends[i].totalWeight = 0;
                this.blends[i].count = 0;
            }

            // collecting...
            for (int i = 0; i < this.weightPacks.Length; i++) {
                var value = this.weightPacks[i].weight.Value;
                if (value > 0) {
                    var facedata = this.database.FaceDataList[weightPacks[i].index];
                    for (int j = 0; j < facedata.MMD4Morphs.Length; j++) {
                        var morph = facedata.MMD4Morphs[j];
                        var bld = this.blends[morph.morphIndex];
                        bld.totalWeight += morph.weight * value * value;
                    }
                    for (int j = 0; j < this.blends.Length; j++) {
                        this.blends[j].count += value;
                    }
                }
            }
        }

        WeightPack[] CollectionWeightComponents() {
            var weights = GetComponentsInChildren<Weight>();
            var weightPacks = new WeightPack[weights.Length];
            for (int i = 0; i < weights.Length; i++) {
                weightPacks[i] = new WeightPack();
                var pack = weightPacks[i];
                pack.index = this.database.GetFaceIndexByGuid(weights[i].FaceGUID);
                pack.weight = weights[i];
                //weights[i].gameObject.SetActive(false);
            }

            return weightPacks;
        }

        /// <summary>
        /// 利用される可能性のあるHelperを作成します
        /// </summary>
        MMD4MecanimMorphHelper[] CreateHelper() {
            var helpers = new MMD4MecanimMorphHelper[this.morphDataLength];

            var existHelperList = new List<MMD4MecanimMorphHelper>(this.GetComponents<MMD4MecanimMorphHelper>());
            for (int i = 0; i < existHelperList.Count; i++) {
                if (!existHelperList[i].enabled) {
                    existHelperList.RemoveAt(i);
                    i--;
                    continue;
                }
            }

            foreach (var idx in GetRequiredMorpList()) {
                int findIdx = -1;
                for (int i = 0; i < existHelperList.Count; i++) {
                    if (existHelperList[i].morphName == getMorphName(idx)) {
                        findIdx = i;
                        break;
                    }
                }
                if (findIdx == -1) {
                    var helper = this.gameObject.AddComponent<MMD4MecanimMorphHelper>();
                    helper.morphName = getMorphName(idx);
                    helper.morphSpeed = 0;
                    helper.morphWeight = 0;
                    helper.overrideWeight = this.OverrideWeight;
                    helpers[idx] = helper;
                } else {
                    var helper = existHelperList[findIdx];
                    helper.morphSpeed = 0;
                    helper.morphWeight = 0;
                    helper.overrideWeight = this.OverrideWeight;
                    helpers[idx] = helper;
                }
            }

            return helpers;
        }

        /// <summary>
        /// 利用される可能性のあるMorphのIndex配列を取得します。<br/>
        /// モーフ番号１と３のモーフが利用される可能性がある場合は [1,3]
        /// </summary>
        protected int[] GetRequiredMorpList() {
            var morphDataList = this.mmdmodel.modelData.morphDataList;
            var morphIdxFlgs = new bool[morphDataList.Length];
            foreach (var fdata in this.database.FaceDataList) {
                foreach (var morph in fdata.MMD4Morphs) {
                    morphIdxFlgs[morph.morphIndex] = true;
                }
            }

            var idxList = new List<int>();
            for (int i = 0; i < morphIdxFlgs.Length; i++) {
                if (morphIdxFlgs[i])
                    idxList.Add(i);
            }

            return idxList.ToArray();
        }

        /// <summary>
        /// index値からモーフ名を取得します
        /// </summary>
        /// <param name="morphIndex"></param>
        /// <returns></returns>
        protected string getMorphName(int morphIndex) {
            string name = null;
            switch (this.mmdmodel.editorViewMorphNameType) {
                case MMD4MecanimModel.EditorViewMorphNameType.Japanese:
                    name = this.mmdmodel.modelData.morphDataList[morphIndex].nameJp;
                    break;
                case MMD4MecanimModel.EditorViewMorphNameType.English:
                    name = this.mmdmodel.modelData.morphDataList[morphIndex].nameEn;
                    break;
                case MMD4MecanimModel.EditorViewMorphNameType.Translated:
                    name = this.mmdmodel.modelData.morphDataList[morphIndex].translatedName;
                    break;
            }
            return name;
        }
    }
}