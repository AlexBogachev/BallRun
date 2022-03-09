using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SectionsFactory : MonoBehaviour
{
    public static SectionsFactory Instance;

    [SerializeField] GameObject sectionPrefab;
    List<GameObject> sections;

    private void Awake()
    {
        sections = new List<GameObject>();

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GameManager.Instance.gameRestarted.AddListener(DestroySections);
    }

    public void BuildSectionsOnStart()
    {
        GameObject newSectionObject = Instantiate(sectionPrefab);
        Section section = newSectionObject.GetComponent<Section>();

        newSectionObject.transform.position = new Vector3(0.0f, -0.56f, 8.3f);
        section.Initialize();
        section.SectionIsOutsidePlayArea.AddListener(RebuildSections);

        sections.Add(newSectionObject);

        BuildLastSection();
        BuildLastSection();
    }

    private void RebuildSections()
    {
        BuildLastSection();
        DestroyFirstSection();
    }

    private void BuildLastSection()
    {
        GameObject newSectionObject = Instantiate(sectionPrefab);
        Section section = newSectionObject.GetComponent<Section>();

        newSectionObject.transform.position = sections.Last().GetComponent<Section>().GetNewSectionSpawnPoint();
        section.Initialize();
        section.SectionIsOutsidePlayArea.AddListener(RebuildSections);

        sections.Add(newSectionObject);
        
    }

    private void DestroyFirstSection()
    {
        sections[0].GetComponent<Section>().SectionIsOutsidePlayArea.RemoveAllListeners();
        Destroy(sections[0]);
        sections.RemoveAt(0);
    }

    private void DestroySections()
    {
        DestroyFirstSection();
        DestroyFirstSection();
        DestroyFirstSection();
    }
}
